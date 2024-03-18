import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { RegisterDto, WebApiClient } from '../services/web-api-client.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit, OnDestroy {
  registerForm!: FormGroup;
  submitted = false;
  user: RegisterDto;
  invalidUser = false;
  invalidEmail = false;
  invalidPass = false;
  invalidPhoneNumber = false;
  errorMessage = '';
  serverErrorMessage = '';
  isLoading = false;
  formSubscription!: Subscription;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private webApiClient: WebApiClient
  ) {
    this.user = new RegisterDto();
  }
  ngOnDestroy(): void {
    if (this.formSubscription) {
      this.formSubscription.unsubscribe();
    }
  }

  initializeForm() {
    this.registerForm = this.formBuilder.group({
      name: ['', Validators.required],
      surname: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.required],
      password: ['', [Validators.required]],
      repeat: ['', Validators.required],
    });
  }

  ngOnInit() {
    this.initializeForm();
  }

  async onSubmit() {
    this.submitted = true;
    this.errorMessage = '';
    this.serverErrorMessage = '';

    if (this.registerForm.invalid) {
      this.errorMessage = 'Please enter all fields!';
      return;
    }

    if (this.registerForm.value.password !== this.registerForm.value.repeat) {
      this.errorMessage = 'Passwords do not match';
      this.invalidPass = true;
      return;
    }

    this.invalidPass = false;
    this.invalidUser = false;
    this.invalidEmail = false;
    this.invalidPhoneNumber = false;

    this.user.name = this.registerForm.value.name;
    this.user.surname = this.registerForm.value.surname;
    this.user.email = this.registerForm.value.email;
    this.user.phoneNumber = this.registerForm.value.phoneNumber;
    this.user.password = this.registerForm.value.password;
    this.isLoading = true;

    this.formSubscription = await this.webApiClient
      .account_Register(this.user)
      .subscribe(
        (response) => {
          this.router.navigate(['/login']);
          this.isLoading = false;
        },
        (err) => {
          this.isLoading = false;
          switch (err.status) {
            case 400: {
              this.invalidUser = true;
              this.errorMessage = 'Email already exists';
              break;
            }
            case 403: {
              this.invalidEmail = true;
              this.errorMessage = 'Email already exists';
              break;
            }
            default: {
              this.serverErrorMessage = 'No connection to server';
              break;
            }
          }
        }
      );
  }
}
