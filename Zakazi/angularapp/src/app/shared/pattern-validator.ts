
import { AbstractControl, ValidatorFn, ValidationErrors } from '@angular/forms';

export function patternValidator(regexp: RegExp): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const value = control.value;
    if (value === '') {
      return null;
    }
    return !regexp.test(value) ? { 'patternInvalid': { regexp } } : null;
  };
}

export function uppercaseValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value as string;
    if (!/[A-Z]/.test(value)) {
      return { uppercase: true };
    }
    return null;
  };
}

// Custom validator to check if the password contains at least one lowercase letter
export function lowercaseValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value as string;
    if (!/[a-z]/.test(value)) {
      return { lowercase: true };
    }
    return null;
  };
}

// Custom validator to check if the password contains at least one digit or special character
export function digitOrSpecialCharValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value as string;
    if (!/[0-9!@#$%^&*()_+[\]{};:'"<>,.?~\\/-]/.test(value)) {
      return { digitOrSpecialChar: true };
    }
    return null;
  };
}

// Custom validator to check if the password and repeat password match
export function passwordMatchValidator(passwordKey: string, repeatPasswordKey: string): ValidatorFn {
  return (group: AbstractControl): ValidationErrors | null => {
    const password = group.get(passwordKey)?.value;
    const repeatPassword = group.get(repeatPasswordKey)?.value;
    if (!password || !repeatPassword || password !== repeatPassword) {
      return { passwordMatch: true };
    }
    return null;
  };
}