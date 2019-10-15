export enum DomainVerificationType {
    dns = 3,
    file = 2,
    manually = 1
}

export const DomainVerificationTypeStrings = {
    [DomainVerificationType.dns] : 'DOMAIN_VERIFICATION_ENUM.DNS',
    [DomainVerificationType.file] : 'DOMAIN_VERIFICATION_ENUM.FILE',
    [DomainVerificationType.manually] : 'DOMAIN_VERIFICATION_ENUM.MANUALLY'
}
