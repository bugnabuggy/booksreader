import { ActionType } from "@br/core/enums";

export interface Action<T> {
    type: ActionType;
    data: T;
}
