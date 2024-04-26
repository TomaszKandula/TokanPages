import axios from "axios";
import { ApplicationAction } from "../../Configuration";
import { ReAuthenticateUserDto } from "../../../Api/Models";
import { NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { UPDATE, TKnownActions as TUpdateActions } from "./userDataStore";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { REAUTHENTICATE as REAUTHENTICATE_USER, RequestContract, GetConfiguration } from "../../../Api/Request";

export const REAUTHENTICATE = "REAUTHENTICATE_USER";
export const CLEAR = "REAUTHENTICATE_USER_CLEAR";
export const RESPONSE = "REAUTHENTICATE_USER_RESPONSE";
interface ReAuthenticate {
    type: typeof REAUTHENTICATE;
}
interface Clear {
    type: typeof CLEAR;
}
interface Response {
    type: typeof RESPONSE;
    payload: object;
}
export type TKnownActions = ReAuthenticate | Clear | Response | TUpdateActions;

//TODO: refactor, simplify
export const UserReAuthenticateAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    reAuthenticate:
        (refreshToken: string, userId: string): ApplicationAction<TKnownActions> =>
        dispatch => {
            dispatch({ type: REAUTHENTICATE });

            const payload: ReAuthenticateUserDto = {
                userId: userId,
                refreshToken: refreshToken,
            };

            const request: RequestContract = {
                configuration: {
                    method: "POST",
                    url: REAUTHENTICATE_USER,
                    data: payload,
                },
            };

            axios(GetConfiguration(request))
                .then(response => {
                    if (response.status === 200) {
                        const pushData = () => {
                            dispatch({ type: RESPONSE, payload: response.data });
                            dispatch({ type: UPDATE, payload: response.data });
                        };

                        return response.data === null
                            ? RaiseError({ dispatch: dispatch, errorObject: NULL_RESPONSE_ERROR })
                            : pushData();
                    }

                    RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
                })
                .catch(error => {
                    RaiseError({ dispatch: dispatch, errorObject: error });
                });
        },
};
