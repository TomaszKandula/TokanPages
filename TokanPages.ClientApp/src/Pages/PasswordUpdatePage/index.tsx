import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ContentPageDataAction } from "../../Store/Actions";
import { UpdatePassword } from "../../Components/Account";
import { Navigation } from "../../Components/Layout";
import { Colours } from "../../Theme";

export const PasswordUpdatePage = (): React.ReactElement => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentPageDataAction.request(["navigation", "templates", "updatePassword"]));
    }, [language?.id]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <UpdatePassword pt={15} pb={30} background={{ backgroundColor: Colours.colours.lightGray3 }} />
        </>
    );
};
