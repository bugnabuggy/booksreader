import { HttpClient } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { SiteConstants } from '@br/config';

export function createTranslateLoader(http: HttpClient) {
    return new TranslateHttpLoader(http, SiteConstants.localizationDictionariesPath, '.json');
}
