import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AdventureService } from '../_services/adventures/adventureService';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(
    public accountService: AccountService,
    private router: Router,
    private toastr: ToastrService,
    private adventureService: AdventureService
  ) {}
  ngOnInit(): void {}

  login() {
    this.accountService.login(this.model).subscribe({
      next: () => {
        //Send to a useful page once logged in
        this.router.navigateByUrl('/playercharacters');
        this.model = {};
      },
    });
  }

  logout() {
    this.accountService.logout();
    //Send back home
    this.router.navigateByUrl('/');
  }

  register() {
    this.router.navigateByUrl('/register');
  }

  resetSaves() {
    this.adventureService.reset()
      .subscribe({
        next: () => this.toastr.success('Reset Adventure'),
        error: (error) => this.toastr.error(error),
      });
  }
}
