import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { FormGroup, FormControl, FormBuilder, Validators, FormArray } from '@angular/forms';
import { AuthService } from 'src/app/_services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Platform } from 'src/app/_models/platform';
import { Mission } from 'src/app/_models/mission';
import { Businessfunction } from 'src/app/_models/businessfunction';
import { Industry } from 'src/app/_models/industry';
import { MissionCreateFormLists } from 'src/app/_models/missioncreateformlists';
import { Accelerator } from 'src/app/_models/accelerator';
import { MissionService } from 'src/app/_services/mission.service';

@Component({
  selector: 'app-mission-create',
  templateUrl: './mission-create.component.html',
  styleUrls: ['./mission-create.component.css']
})
export class MissionCreateComponent implements OnInit {

  @Output() cancelCreateMission = new EventEmitter();
  mission: Mission;
  platforms: Platform[];
  businessFunctions: Businessfunction[];
  industries: Industry[];
  accelerators: Accelerator[];
  missionCreateFormLists: MissionCreateFormLists;
  missionForm: FormGroup;
  showAccelerators = false;

  constructor(private authService: AuthService, private missionService: MissionService, private router: Router,
    private alertify: AlertifyService, private route: ActivatedRoute, private fb: FormBuilder) { }

  ngOnInit() {
     this.route.data.subscribe(data => {
      this.missionCreateFormLists = data['missioncreateformlists'];
    });

    this.createMissionForm();
  }

  createMissionForm() {
    this.platforms = this.missionCreateFormLists.platforms;
    this.businessFunctions = this.missionCreateFormLists.businessFunctions;
    this.industries = this.missionCreateFormLists.industries;

    const platformFormControls = this.platforms.map(control => new FormControl(false));

    this.missionForm = this.fb.group({
      missionname: [''],
      challenge: [''],
      missionplatforms: new FormArray(platformFormControls),
      desiredoutcome: [''],
      timeframe: [''],
      businessfunctionid: [''],
      industryid: [''],
      public: [''],
      missionaccelerators: new FormArray([])
    });

    // BusinessFunction and Industry ID are 1000 in the DB if the DB
    // has something different this form will not work properly
    this.missionForm.controls.businessfunctionid.patchValue('1000');
    this.missionForm.controls.industryid.patchValue('1000');
  }

  createMission() {
    if (this.missionForm.valid) {
      this.mission = Object.assign({}, this.missionForm.value, {
        missionplatforms: this.missionForm.value.missionplatforms
          .map((checked, index) => checked ? {platformid: this.platforms[index].id} : null)
          .filter(value => value !== null),
        missionaccelerators: this.missionForm.value.missionaccelerators
          .map((checked, index) => checked ? {acceleratorid: this.accelerators[index].id} : null)
          .filter(value => value !== null)
        });

      this.missionService
        .createMission(this.authService.decodedToken.nameid, this.mission).subscribe(() => {
          this.alertify.success('Mission Created Successfully');
          this.router.navigate(['/missions']);
        }, error => {
          this.alertify.error(error);
        });
    }
      console.log(this.mission);
  }

  onOptionBusinessFunctionSelected(businessFunctionId: number) {
    this.clearAcceleratorsCheckboxItems();

    this.missionService
      .getAcceleratorsByIndustryAndBusinessId(this.authService.decodedToken.nameid, businessFunctionId, 0)
      .subscribe((accelerators: Accelerator[]) => {
        this.accelerators = accelerators;
        this.accelerators.map((o, i) => {
          const control = new FormControl(false);
          (this.missionForm.controls.missionaccelerators as FormArray).push(control);
        });
        this.showAccelerators = true;
    }, error => {
      this.alertify.error(error);
    });
  }

  onOptionIndustrySelected(industryId: number) {
    this.clearAcceleratorsCheckboxItems();

    this.missionService
      .getAcceleratorsByIndustryAndBusinessId(this.authService.decodedToken.nameid, 0, industryId)
      .subscribe((accelerators: Accelerator[]) => {
        this.accelerators = accelerators;
        this.accelerators.map((o, i) => {
          const control = new FormControl(false);
          (this.missionForm.controls.missionaccelerators as FormArray).push(control);
        });
        this.showAccelerators = true;
    }, error => {
      this.alertify.error(error);
    });
  }

  clearAcceleratorsCheckboxItems() {
    const controlCount =  (this.missionForm.controls.missionaccelerators as FormArray).length;
    const acceleratorFormArray = <FormArray>this.missionForm.controls.missionaccelerators;

    if (controlCount > 0) {
      this.accelerators.forEach(accelerator => {
        const index = acceleratorFormArray.controls.findIndex(x => x.value === accelerator.id);
        (<FormArray>this.missionForm.get('missionaccelerators')).removeAt(index);
        });
      }
  }

  cancel() {
    this.cancelCreateMission.emit(false);

/* 
    if (controlCount > 0) {
      this.accelerators.forEach(accelerator => {
        const index = acceleratorFormArray.controls.findIndex(x => x.value === accelerator.id);
        console.log('Business.removeAt: ' + i + ' - checkboxId: ' + accelerator.id + ' index: ' + index);
        (<FormArray>this.missionForm.get('accelerators')).removeAt(index);
        console.log('Business.Checkbox' + (this.missionForm.controls.accelerators as FormArray));
        });
      } */
  }
}
