import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { UserDataStoreState } from "../../States";
import { USER_DATA } from "../../../Shared/constants";
import base64 from "base-64";
import utf8 from "utf8";

import { DelDataFromStorage, SetDataInStorage } from "../../../Shared/Services/StorageServices";

import { TKnownActions, SHOW, CLEAR, UPDATE } from "../../Actions/Users/userDataStore";

const GetBase64 = (input: object): string => {
    const json = JSON.stringify(input);
    const utf8data = utf8.encode(json);
    return base64.encode(utf8data);
};

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
            SetDataInStorage({ selection: GetBase64(action.payload), key: USER_DATA });
            return {
                isShown: state.isShown,
                userData: {
                    userId: action.payload.userId,
                    isVerified: action.payload.isVerified,
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
