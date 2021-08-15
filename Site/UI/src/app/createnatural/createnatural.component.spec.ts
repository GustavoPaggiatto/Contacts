import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatenaturalComponent } from './createnatural.component';

describe('CreatenaturalComponent', () => {
  let component: CreatenaturalComponent;
  let fixture: ComponentFixture<CreatenaturalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreatenaturalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatenaturalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
