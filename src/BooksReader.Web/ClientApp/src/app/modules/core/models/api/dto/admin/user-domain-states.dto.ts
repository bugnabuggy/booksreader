import { DomainVerificationType } from '@br/core/enums';

export interface UserDomainState {
    domainId: string;
    domainName: string;
    username: string;
    verified: boolean;
    protocol: string;
    type: DomainVerificationType;
    verificationDate?: string;
    verificationRequested?: string;

    numberOfPages: number;
}