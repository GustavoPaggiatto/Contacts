import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetaillegalpersonComponent } from './detaillegalperson.component';

describe('DetaillegalpersonComponent', () => {
  let component: DetaillegalpersonComponent;
  let fixture: ComponentFixture<DetaillegalpersonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetaillegalpersonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetaillegalpersonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
