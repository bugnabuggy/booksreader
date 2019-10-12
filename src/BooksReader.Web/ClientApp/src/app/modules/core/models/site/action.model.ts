import { ActionType } from "@br/core/enums";

export interface Action<T> {
    action: ActionType;
    data: T
}
