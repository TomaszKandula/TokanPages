import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import { IApplicationState } from "../../Store/applicationState";
import { ActionCreators as NavigationContent } from "../../Store/Actions/Content/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../../Store/Actions/Content/getFooterContentAction";
import { ActionCreators as SigninFormContent } from "../../Store/Actions/Content/getUserSigninContentAction";
import { Navigation } from "../../Components/Layout";
import { Footer } from "../../Components/Layout";
import { UserSignin } from "../../Components/Account";

export const SigninPage = (): JSX.Element => 
{  
    const dispatch = useDispatch();
    
    const language = useSelector((state: IApplicationState) => state.userLanguage);
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const signinForm = useSelector((state: IApplicationState) => state.getUserSigninContent);

    React.useEffect(() => 
    {
        dispatch(NavigationContent.getNavigationContent());
        dispatch(FooterContent.getFooterContent());
        dispatch(SigninFormContent.getUserSigninContent());
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
