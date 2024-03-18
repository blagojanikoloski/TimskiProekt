import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { LoginDto, WebApiClient } from '../services/web-api-client.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit, OnDestroy{
  loginForm!: FormGroup;
  submitted = false;
  user: LoginDto;
  invalidUser = false;
  invalidPass = false;
  errorMessage = '';
  isLoading = false;
  serverErrorMessage = '';
  formSubscription!: Subscription;
  forgotPasswordClicked = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private webApiClient: WebApiClient,
    // private accountService: AccountService,
  ) {
    this.user = new LoginDto();
  }
  ngOnInit() {
    this.initializeForm();
  }
  
  ngOnDestroy(): void {
    if(this.formSubscription){
      this.formSubscription.unsubscribe();
    }
  }

  initializeForm() {
    this.loginForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
  }


  async onSubmit() {
    this.submitted = true;
    this.errorMessage = '';
    this.serverErrorMessage = '';
    if (this.loginForm.invalid) {
      this.errorMessage = 'Please enter all fields!';
      return;
    }

    this.isLoading = true;
    this.invalidPass = false;
    this.invalidUser = false;

    this.user.email = this.loginForm.value.email;
    this.user.password = this.loginForm.value.password;
    this.formSubscription = await this.webApiClient.account_Login(this.user).subscribe(
      (response) => {
        localStorage.setItem('user', JSON.stringify(response));
        // this.accountService.setCurrentUser();
        this.isLoading = false;
        this.router.navigate(['/home']);
      },
      (err) => {
        this.isLoading = false;
        switch (err.status) {
          case 401: {
            this.invalidUser = true;
            this.invalidPass = true;
            this.errorMessage = 'Invalid Email or Password';
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
