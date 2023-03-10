import { Component, Inject } from '@angular/core';
import { Tab } from '../controls/tab';
import { UserComponent } from '../controls/user.component';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html'
})
export class AdminComponent {
  tabs: Tab[] = [
    {
      title: 'Users',
      active: true,
      iconClass: 'fab fa-html5',
      content: '<app-admin-user></app-admin-user>'
    },
    {
      title: 'CSS',
      active: false,
      iconClass: 'fab fa-css3',
      content: `<strong>Cascading Style Sheets(CSS)</strong> is a stylesheet language used to describe
        the presentation of a document written in HTML or XML (including XML dialects
        such as SVG, MathML or XHTML). CSS describes how elements should be rendered on screen,
        on paper, in speech, or on other media.`
    }
  ];
}


