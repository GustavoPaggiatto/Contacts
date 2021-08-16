import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdatelegalpersonComponent } from './updatelegalperson.component';

describe('UpdatelegalpersonComponent', () => {
  let component: UpdatelegalpersonComponent;
  let fixture: ComponentFixture<UpdatelegalpersonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UpdatelegalpersonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdatelegalpersonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
