import { DomainVerificationType } from '@br/core/enums';

export interface UserDomain {
    id: string;
    protocol: string;
    name: string;
    verified: boolean;
    verificationType: DomainVerificationType;
    verificationCode: string;
}
