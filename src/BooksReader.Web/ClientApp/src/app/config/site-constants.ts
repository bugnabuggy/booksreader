export class SiteConstants {
    static Short_timeout = 5000;
    static itemsPerPage = [5, 10, 50, 100, 500, 1000, 10000];
    static defaultPageSize = 50;

    static defaultLanguage = 'en';
    static defaultImageSelectorHeight = '240px';
    static defaultImage = '/assets/images/default-image.png';

    static storageKeys = {
        userToken: 'userToken',
        uiIsShown: 'uiIsShown',
        userLang: 'userLang'
    }

    static localizationDictionariesPath = './assets/i18n/';

    static lists = {
        сurrency: 'Currency'
    }
}
