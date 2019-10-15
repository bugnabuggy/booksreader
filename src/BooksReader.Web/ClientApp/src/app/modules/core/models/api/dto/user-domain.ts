import { DomainVerificationType } from '@br/core/enums';
import { IIdentifiable } from '../entities';

export interface UserDomain extends IIdentifiable{
    id: string;
    protocol: string;
    name: string;
    verified: boolean;
    verificationType: DomainVerificationType;
    verificationCode: string;

    certificate?: string;
    ownerId?: string;
}
