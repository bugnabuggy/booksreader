import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { environment } from '@br/env/environment';
import { AppModule } from './app/app.module';


export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}

const providers = [
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }
];

if(window) {
  (window as any).tinyMCE.overrideDefaults({
    base_url: '/tinymce/',  // Base for assets such as skins, themes and plugins
    suffix: '.min'          // This will make Tiny load minified versions of all its assets
  });
}


if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic(providers).bootstrapModule(AppModule)
  .catch(err => console.log(err));
