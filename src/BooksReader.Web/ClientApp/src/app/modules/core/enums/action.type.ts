export enum ActionType {
    select = 'select',
    add = 'add',
    edit = 'edit',
    delete = 'delete',

    verify = 'verify',
    close = 'close'
}

export const ActionTypeStrings = {
    [ActionType.add] : "ACTION_TYPE_ADD"
}