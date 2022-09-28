import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../Store/applicationState";
import { ActionCreators as NavigationContent } from "../../Store/Actions/Content/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../../Store/Actions/Content/getFooterContentAction";
import { ActionCreators as AccountContent } from "../../Store/Actions/Content/getAccountContentAction";
import { Navigation } from "../../Components/Layout";
import { Footer } from "../../Components/Layout";
import { UserAccount } from "../../Components/Account";

export const AccountPage = (): JSX.Element => 
{
    const dispatch = useDispatch();
    const language = useSelector((state: IApplicationState) => state.userLanguage);
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const account = useSelector((state: IApplicationState) => state.getAccountContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);

    React.useEffect(() => 
    {
        dispatch(NavigationContent.getNavigationContent());
        dispatch(AccountContent.getAccountContent());
        dispatch(FooterContent.getFooterContent());
    }, 
    [ dispatch, language?.id ]);

    return(
        <>
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <UserAccount content={account?.content} isLoading={navigation?.isLoading} />
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}
