import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { SecurityService } from '../../services/security.service';
import { Group } from '../../model/domain/Group';

@Component({
  selector: 'app-grupos',
  templateUrl: './grupos.component.html',
  styleUrls: ['./grupos.component.css']
})
export class GruposComponent implements OnInit {

  displayedColumns = ['actions', 'name'];
  model: Group[] = null;

  constructor(
    private service: SecurityService,
    private router: Router
  ) { }

  ngOnInit() {
    this.getGroups();
  }

  getGroups() {
    this.service.GetGroups().subscribe(q => {
      this.model = q;
    });
  }

  openGroup(id: number = null) {
    if (id === null) {
      this.router.navigate(['/grupos-cadastro']);
    } else {
      this.router.navigate(['/grupos-cadastro', id]);
    }
  }
}
