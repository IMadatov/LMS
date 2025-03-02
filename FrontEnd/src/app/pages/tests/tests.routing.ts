import { Route } from '@angular/router';
import { TestComponent } from './test/test.component';
import { TestsComponent } from './tests.component';

export const TESTS_ROUTES: Route[] = [
  { path: '', component: TestsComponent },
  { path: 'test', component: TestComponent },
];
