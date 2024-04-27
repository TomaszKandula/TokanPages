import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useInterval } from "../../../Shared/Hooks";
import { ApplicationState } from "../../../Store/Configuration";
import { UserReAuthenticateAction, UserDataStoreAction } from "../../../Store/Actions";
import { JWT } from "../../../Api/Models";
import Validate from "validate.js";
import jwtDecode from "jwt-decode";

interface Properties {
    children: React.ReactNode;
}

export const ApplicationSession = (props: Properties): JSX.Element => {
    const dispatch = useDispatch();
    const store = useSelector((state: ApplicationState) => state.userDataStore);
    const [expiration, setExpiration] = React.useState<number | undefined>(undefined);

    const sessionTimer = () => {
        if (!expiration) return;

        const currentDateTime = new Date().getTime() / 1000;
        if (currentDateTime >= expiration) {
            const refreshToken = store?.userData?.refreshToken;
            const userId = store?.userData?.userId;

            dispatch(UserDataStoreAction.clear());
            dispatch(UserReAuthenticateAction.reAuthenticate(refreshToken, userId));

            setExpiration(undefined);
        }
    };

    React.useEffect(() => {
        const userToken = store?.userData?.userToken;
        if (Validate.isEmpty(userToken)) {
            if (expiration) {
                setExpiration(undefined);
            }

            return;
        }

        if (!expiration) {
            const decoded = jwtDecode<JWT>(userToken);
            setExpiration(decoded.exp);
        }
    }, [store?.userData?.userToken, expiration]);

    useInterval(() => sessionTimer(), 5000);
    return <>{props.children}</>;
};
