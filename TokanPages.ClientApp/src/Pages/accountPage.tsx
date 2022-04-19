import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Navigation from "../Components/Layout/navigation";
import Footer from "../Components/Layout/footer";
import UserAccount from "../Components/Account/userAccount";
import { IApplicationState } from "../Redux/applicationState";
import { ActionCreators as NavigationContent } from "../Redux/Actions/Content/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../Redux/Actions/Content/getFooterContentAction";

const AccountPage = (): JSX.Element => 
{
    const dispatch = useDispatch();
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);

    React.useEffect(() => 
    {
        dispatch(NavigationContent.getNavigationContent());
        dispatch(FooterContent.getFooterContent());
    }, 
    [ dispatch ]);

    return(
        <>
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <UserAccount />
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}

export default AccountPage;
