import { Component, OnInit } from '@angular/core';
import { BsModalRef } from '../../../../node_modules/ngx-bootstrap';
import { Accelerator } from '../../_models/accelerator';
import { UseCase } from 'src/app/_models/usecase';


@Component({
  selector: 'app-accelerator-use-case-modal',
  templateUrl: './accelerator-use-case-modal.component.html',
  styleUrls: ['./accelerator-use-case-modal.component.css']
})
export class AcceleratorUseCaseModalComponent implements OnInit {
  accelerator: Accelerator;
  usecase: UseCase;

  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit() {
    const jsonDescription: any = JSON.parse(this.accelerator.description); // string to generic object first
    this.usecase = <UseCase>jsonDescription;
  }

}
