import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatelegalComponent } from './createlegal.component';

describe('CreatelegalComponent', () => {
  let component: CreatelegalComponent;
  let fixture: ComponentFixture<CreatelegalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreatelegalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatelegalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
