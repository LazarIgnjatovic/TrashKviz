import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MaterialModule } from '../core/modules/material/material.module';
import { ButtonComponent } from './components/button/button.component';
import { ClassicListComponent } from './components/classic-list/classic-list.component';
import { FormChecklistWithSectionsComponent } from './components/form-checklist-with-sections/form-checklist-with-sections.component';
import { FormDateRangeComponent } from './components/form-date-range/form-date-range.component';
import { FormInputComponent } from './components/form-input/form-input.component';
import { FormComponent } from './components/form/form.component';
import { HeaderComponent } from './components/header/header.component';
import { MatCardComponent } from './components/mat-card/mat-card.component';
import { SelectListComponent } from './components/select-list/select-list.component';
import { DialogComponent } from './components/dialog/dialog.component';

const components = [
  FormInputComponent,
  MatCardComponent,
  FormComponent,
  FormDateRangeComponent,
  FormDateRangeComponent,
  SelectListComponent,
  ButtonComponent,
  ClassicListComponent,
  FormChecklistWithSectionsComponent,
  DialogComponent,
  HeaderComponent,
];

@NgModule({
  declarations: [components],
  imports: [CommonModule, ReactiveFormsModule, MaterialModule, RouterModule],
  exports: [ReactiveFormsModule, components],
})
export class SharedModule {}
