import { NavigationItem } from '../../models/navigation-item';
import { Roles } from '../../models/roles';

export const defaultNavigation: NavigationItem[] = [
  {
    key: [Roles.Admin, Roles.Student, Roles.Teacher],
    label: '',
    hidden: true
  },
  {
    key: [Roles.Admin, Roles.Student, Roles.Teacher],
    label: 'Dashboard',
    icon: 'pi pi-gauge',
    command: 'dashboard',
    hidden: false
  },
  {
    key: [Roles.Admin],
    label: 'usersKey',
    icon: 'pi pi-users',
    hidden: false,
    items: [
      {
        key: [Roles.Admin],
        label: 'allKey',
        icon: '',
        command: 'admin/allusers',
        hidden: false
      },
      {
        key: [Roles.Admin],
        label: 'teachersKey',
        icon: '',
        command: 'admin/allusers/teachers',
        hidden: false
      },
      {
        key: [Roles.Admin],
        label: 'studentsKey',
        icon: '',
        command: 'admin/allusers/students',
        hidden: false
      },
      {
        key: [Roles.Admin],
        label: 'adminsKey',
        icon: '',
        command: 'admin/allusers/admins',
        hidden: false
      },
    ],
  },
  {
    key: [Roles.Student],
    label: 'subjectsKey',
    icon: 'pi pi-book',
    command: 'student/subjects',
    hidden: false,
    items: [
      {
        key: [Roles.Student],
        label: 'subjectKey',
        icon: '',
        command: 'student/subjects/subject',
        hidden: true
      }
    ]
  },
  {
    key: [Roles.Student],
    label: 'testsKey',
    icon: 'pi pi-list-check',
    command: 'student/tests',
    hidden: false,
    items: [
      {
        key: [Roles.Student],
        label: 'testKey',
        icon: '',
        command: 'student/tests/test',
        hidden: true
      }
    ]
  },
  {
    key: [Roles.Teacher],
    label: 'myclassesKey',
    icon: 'pi pi-th-large',
    command: 'teacher/classes',
    hidden: false,
    items: [
      {
        key: [Roles.Teacher],
        command: 'teacher/classes/:id',
        hidden: true
      }
    ]
  },
  {
    key: [Roles.Teacher],
    label: 'mysubjectsKey',
    icon: 'pi pi-address-book',
    command: 'teacher/subjects',
    hidden: false
  },

  {
    key: [Roles.Admin],
    label: 'Transloco',
    icon:'pi pi-language',
    hidden:false,
    command: 'admin/transloco',
  },
  
  {
    key: [Roles.Admin, Roles.Student, Roles.Teacher],
    label: 'profileKey',
    icon: 'pi pi-user',
    hidden: false,
    items: [
      {
        key: [Roles.Admin, Roles.Student, Roles.Teacher],
        label: 'settingsKey',
        icon: 'pi pi-cog',
        command: 'profile',
        hidden: false,
      },
      {
        key: [Roles.Admin, Roles.Student, Roles.Teacher],
        label: 'logoutKey',
        icon: 'pi pi-sign-out',
        command: 'logout',
        hidden: false,
      },
    ],
  },
];
