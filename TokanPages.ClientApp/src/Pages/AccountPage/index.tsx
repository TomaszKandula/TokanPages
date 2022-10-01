import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { UserAccount } from "../../Components/Account";

import { 
    ContentNavigationAction, 
    ContentFooterAction,
    ContentAccountAction 
} from "../../Store/Actions";

export const AccountPage = (): JSX.Element => 
{
    const dispatch = useDispatch();
    const language = useSelector((state: IApplicationState) => state.applicationLanguage);
    const navigation = useSelector((state: IApplicationState) => state.contentNavigation);
    const account = useSelector((state: IApplicationState) => state.contentAccount);
    const footer = useSelector((state: IApplicationState) => state.contentFooter);

    React.useEffect(() => 
    {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentAccountAction.get());
        dispatch(ContentFooterAction.get());
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
