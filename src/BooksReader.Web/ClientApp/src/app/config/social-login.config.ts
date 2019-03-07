import { AuthServiceConfig, LoginOpt } from 'angularx-social-login';
import { GoogleLoginProvider, FacebookLoginProvider, LinkedInLoginProvider} from 'angularx-social-login';
import { environment } from '../../environments/environment';


const fbLoginOptions: LoginOpt = {
  scope: 'public_profile, email, phone',
  return_scopes: true,
  enable_profile_selector: true
}; // https://developers.facebook.com/docs/reference/javascript/FB.login/v2.11

export const SOCIAL_CONFIG = new AuthServiceConfig([
    // {
    //   id: GoogleLoginProvider.PROVIDER_ID,
    //   provider: new GoogleLoginProvider('Google-OAuth-Client-Id')
    // },
    {
      id: FacebookLoginProvider.PROVIDER_ID,
      provider: new FacebookLoginProvider(environment.facebookAppId)
    },
    // {
    //   id: LinkedInLoginProvider.PROVIDER_ID,
    //   provider: new LinkedInLoginProvider('LinkedIn-client-Id', false, 'en_US')
    // }
  ]);

