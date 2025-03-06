import { Component, OnInit, Output, ViewChild } from '@angular/core';
import { DrawerModule } from 'primeng/drawer';
import { ButtonModule } from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { AvatarModule } from 'primeng/avatar';
import { StyleClassModule } from 'primeng/styleclass';
import { Sidebar } from 'primeng/sidebar';
import { HeaderService } from '../header.service';
import { PanelMenuModule } from 'primeng/panelmenu';
import { MenuItem } from 'primeng/api';
import { MenuModule } from 'primeng/menu';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { BadgeModule } from 'primeng/badge';
import { NavigationItem } from '../../../models/navigation-item';
import { AuthService } from '../../../pages/auth/auth.service';
import { TranslocoModule } from '@jsverse/transloco';
@Component({
    selector: 'app-side-bar',
    imports: [
        CommonModule,
        DrawerModule,
        ButtonModule,
        RippleModule,
        AvatarModule,
        StyleClassModule,
        PanelMenuModule,
        MenuModule,
        BadgeModule,
        TranslocoModule
    ],
    templateUrl: './side-bar.component.html',
    styleUrl: './side-bar.component.css',
    standalone:true
})
export class SideBarComponent implements OnInit {
  constructor(
    public headerService: HeaderService,
    private router: Router,
    public authService: AuthService,
  ) {}

  @ViewChild('sidebarRef') @Output() sidebarRef!: Sidebar;

  closeCallback(e: any): void {
    this.sidebarRef.close(e);
    this.headerService.DeActiveSideBar();
  }

  items: MenuItem[] | undefined;
  itemsProfile: MenuItem[] | undefined;

  ngOnInit(): void {
    this.items = this.mapNavigationItems(this.authService.navigationPages!)

  }

  mapNavigationItems(navigationItems: NavigationItem[]) {
    return navigationItems.filter(x=>x.hidden==false).map((x) => {
      const mappedItem: any = {
        label: x.label || 'null',
        icon: x.icon || '',
        items: x.items ? this.mapNavigationItems(x.items) : undefined,
        command: x.command
          ? () => {
            if(x.command!='logout'){
              this.router.navigateByUrl(x.command!);
            }
            if(x.command=='logout'){
              this.headerService.LogOut();
            }
            }
            
          : undefined,
      };

      return mappedItem;
    });
  }  
}
/*
[
  {
    "label": "Dashboard",
    "icon": "pi pi-gauge"
  },
  {
    "label": "Users",
    "icon": "pi pi-users",
    "items": [
      {
        "label": "All users",
        "icon": ""
      },
      {
        "label": "Teachers",
        "icon": ""
      },
      {
        "label": "Students",
        "icon": ""
      },
      {
        "label": "Admins",
        "icon": ""
      }
    ]
  },
  {
    "label": "Profile",
    "icon": "pi pi-user",
    "items": [
      {
        "label": "Settings",
        "icon": "pi pi-cog"
      },
      {
        "label": "Log out",
        "icon": "pi pi-sign-out"
      }
    ]
  }
]
*/