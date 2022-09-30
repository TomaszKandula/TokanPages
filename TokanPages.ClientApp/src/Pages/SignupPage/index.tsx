import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import { IApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { UserSignup } from "../../Components/Account";

import { 
    GetNavigationContentAction, 
    GetFooterContentAction, 
    GetUserSignupContentAction 
} from "../../Store/Actions";

export const SignupPage = (): JSX.Element =>
{
    const dispatch = useDispatch();
    
    const language = useSelector((state: IApplicationState) => state.userLanguage);
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const signupForm = useSelector((state: IApplicationState) => state.getUserSignupContent);

    React.useEffect(() => 
    {
        dispatch(GetNavigationContentAction.get());
        dispatch(GetFooterContentAction.get());
        dispatch(GetUserSignupContentAction.get());
    }, 
    [ dispatch, language?.id ]);

    return (
        <>
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <Container>
                <UserSignup content={signupForm?.content} isLoading={signupForm?.isLoading} />
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}
