import { ValidatorFn, FormGroup, ValidationErrors } from '@angular/forms';

/** A hero's name can't match the hero's alter ego */
export const passwordConfirmationValidator: ValidatorFn = (control: FormGroup): ValidationErrors | null => {
    const newPassword = control.get('password') || control.get('newPassword');
    const confirmation = control.get('confirmation');
    
    const val =  newPassword.value && confirmation.value && newPassword.value !== confirmation.value 
    ? { 'passwordConfirmation': true } 
    : null;

    if(val) {
      confirmation.setErrors(val) ;
    }
    
    return val;
  };