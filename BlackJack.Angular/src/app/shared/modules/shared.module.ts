import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

@NgModule({
    imports: [
        CommonModule,
        HttpClientModule,
        FormsModule
    ],
    exports: [
        CommonModule,
        HttpClientModule,
        FormsModule
    ]
})
export class SharedModule { }
