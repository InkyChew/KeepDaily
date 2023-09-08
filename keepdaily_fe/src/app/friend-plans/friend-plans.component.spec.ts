import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FriendPlansComponent } from './friend-plans.component';

describe('FriendPlansComponent', () => {
  let component: FriendPlansComponent;
  let fixture: ComponentFixture<FriendPlansComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FriendPlansComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FriendPlansComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
