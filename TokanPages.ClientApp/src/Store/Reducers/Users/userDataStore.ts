import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { UserDataStoreState } from "../../States";
import { USER_DATA } from "../../../Shared/constants";

import { DelDataFromStorage, SetDataInStorage } from "../../../Shared/Services/StorageServices";

import { TKnownActions, SHOW, CLEAR, UPDATE } from "../../Actions/Users/userDataStore";

export const UserDataStore: Reducer<UserDataStoreState> = (
    state: UserDataStoreState | undefined,
    incomingAction: Action
): UserDataStoreState => {
    if (state === undefined) return ApplicationDefault.userDataStore;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case SHOW:
            return {
                isShown: action.payload,
                userData: state.userData,
            };

        case CLEAR:
            DelDataFromStorage({ key: USER_DATA });
            return ApplicationDefault.userDataStore;

        case UPDATE:
            const encodedNew = window.btoa(JSON.stringify(action.payload));
            SetDataInStorage({ selection: encodedNew, key: USER_DATA });
            return {
                isShown: state.isShown,
                userData: {
                    userId: action.payload.userId,
                    aliasName: action.payload.aliasName,
                    avatarName: action.payload.avatarName,
                    firstName: action.payload.firstName,
                    lastName: action.payload.lastName,
                    email: action.payload.email,
                    shortBio: action.payload.shortBio,
                    registered: action.payload.registered,
                    userToken: action.payload.userToken,
                    refreshToken: action.payload.refreshToken,
                    roles: action.payload.roles,
                    permissions: action.payload.permissions,
                },
            };

        default:
            return state;
    }
};
