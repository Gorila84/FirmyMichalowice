import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { ReCaptchaV3Service } from 'ng-recaptcha';
import { AlertifyService } from '../_services/alertify.service';
import { MessageService } from '../_services/message.service';
import { RecaptchaService } from '../_services/recaptcha-service.service';


/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}


@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {
  @ViewChild('contactForm') contactForm: NgForm;
  model: any ={};
  singInForm: FormGroup;

  
  constructor(private messageService: MessageService,
              private alertify: AlertifyService,
              private recaptchaV3Service: ReCaptchaV3Service,
              private recaptchaValidationService : RecaptchaService ) { }

  ngOnInit(): void {
  }

  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email,
  ]);

  subjectFormControl = new FormControl('', [
    Validators.required,

  ]);

  contentFormControl = new FormControl('', [
    Validators.required,
    
  ]);
  matcher = new MyErrorStateMatcher();



  get f() { return this.singInForm.controls; }

  sendMessage(form: NgForm){
    if (form.invalid) {
      for (const control of Object.keys(form.controls)) {
        form.controls[control].markAsTouched();
      }
      return;
    }
    if (this.emailFormControl.invalid || this.contentFormControl.invalid || this.subjectFormControl.invalid ) {
      alert('Uzupełnij wymagane dane')
      return;
  }

  this.recaptchaV3Service.execute('importantAction')
  .subscribe((token: string) => {

    this.recaptchaValidationService.check(token).subscribe(data => {
    const result = JSON.parse(JSON.stringify(data));
    if(result['success'])
    {
  this.messageService.sendMessage(this.model).subscribe( data=>{
  if(data){
    this.alertify.success('Wysłano wiadomość');
  }
  else{
    this.alertify.error("Nie wysłano wiadomości.");
  }
  },
  error => {
    this.alertify.error("Nie wysłano wiadomości.");
  })
}
})
  })
}
}

