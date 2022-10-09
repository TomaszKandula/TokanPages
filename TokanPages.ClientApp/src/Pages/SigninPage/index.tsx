import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import { IApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { UserSignin } from "../../Components/Account";

import { 
    ContentNavigationAction, 
    ContentFooterAction, 
    ContentUserSigninAction 
} from "../../Store/Actions";

export const SigninPage = (): JSX.Element => 
{  
    const dispatch = useDispatch();
    const language = useSelector((state: IApplicationState) => state.applicationLanguage);

    const navigation = useSelector((state: IApplicationState) => state.contentNavigation);
    const footer = useSelector((state: IApplicationState) => state.contentFooter);
    const form = useSelector((state: IApplicationState) => state.contentUserSignin);

    React.useEffect(() => 
    {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentUserSigninAction.get());
    }, 
    [ language?.id ]);

    return (
        <>
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <Container>
                <UserSignin content={form?.content} isLoading={form?.isLoading} />
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}
