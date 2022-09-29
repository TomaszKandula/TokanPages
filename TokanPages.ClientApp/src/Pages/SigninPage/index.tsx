import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import { IApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { UserSignin } from "../../Components/Account";

import { 
    GetNavigationContentAction, 
    GetFooterContentAction, 
    GetUserSigninContentAction 
} from "../../Store/Actions";

export const SigninPage = (): JSX.Element => 
{  
    const dispatch = useDispatch();
    
    const language = useSelector((state: IApplicationState) => state.userLanguage);
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const signinForm = useSelector((state: IApplicationState) => state.getUserSigninContent);

    React.useEffect(() => 
    {
        dispatch(GetNavigationContentAction.getNavigationContent());
        dispatch(GetFooterContentAction.getFooterContent());
        dispatch(GetUserSigninContentAction.getUserSigninContent());
    }, 
    [ dispatch, language?.id ]);
    
    return (
        <>
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <Container>
                <UserSignin content={signinForm?.content} isLoading={signinForm?.isLoading} />
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}
