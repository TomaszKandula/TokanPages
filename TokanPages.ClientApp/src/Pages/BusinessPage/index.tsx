import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ContentPageDataAction } from "../../Store/Actions";
import { BusinessForm } from "../../Components/Business";
import { Navigation } from "../../Components/Layout";
import { Colours } from "../../Theme";

export const BusinessPage = () => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentPageDataAction.request(["navigation", "templates", "businessForm"]));
    }, [language?.id]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <BusinessForm
                pt={15}
                pb={30}
                hasCaption={false}
                hasIcon={true}
                hasShadow={true}
                background={{ backgroundColor: Colours.colours.lightGray3 }}
            />
        </>
    );
};
