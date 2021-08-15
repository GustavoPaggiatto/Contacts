import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailnaturalpersonComponent } from './detailnaturalperson.component';

describe('DetailnaturalpersonComponent', () => {
  let component: DetailnaturalpersonComponent;
  let fixture: ComponentFixture<DetailnaturalpersonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetailnaturalpersonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetailnaturalpersonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
