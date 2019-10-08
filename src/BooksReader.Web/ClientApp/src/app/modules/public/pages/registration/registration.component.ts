import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { passwordConfirmationValidator } from '@br/utilities/validators';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  public readonly regForm = this.fb.group({
    username: ['', Validators.required],
    fullname: ['', Validators.required],
    password: ['', [ Validators.required, Validators.minLength(3)]],
    confirmation: ['', [ Validators.required, Validators.minLength(3)]],
    agree: [''],
  }, {validators: passwordConfirmationValidator})

  indeterminate:boolean = false;

  constructor(
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    this.regForm.statusChanges.subscribe(val=>{
      console.log(this.regForm.errors);
    })
  }

}
