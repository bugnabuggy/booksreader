import { ConfirmationType } from '@br/core/enums';
import { DialogModel } from './dialog.model';

export interface ConfirmationDialogModel extends DialogModel{
    type: ConfirmationType;

    yes: string;
    no: string;
    cancel: string;
}