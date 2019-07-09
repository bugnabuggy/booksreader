import { AuthResponse } from '@br/core/models';

export const authMockResponse = {
    access_token: 'accessToken',
    expires_in:3600,
    token_type: 'bearer'
    } as AuthResponse;
