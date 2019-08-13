import { ConfirmationType } from '@br/core/enums';

export interface ConfirmationDialogModel {
    title: string;
    question: string;
    type: ConfirmationType
}
