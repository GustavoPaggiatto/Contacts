import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdatenaturalpersonComponent } from './updatenaturalperson.component';

describe('UpdatenaturalpersonComponent', () => {
  let component: UpdatenaturalpersonComponent;
  let fixture: ComponentFixture<UpdatenaturalpersonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UpdatenaturalpersonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdatenaturalpersonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
