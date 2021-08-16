import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import UserSignout from "../Components/Account/userSignout";
import Footer from "../Components/Layout/footer";
import { IApplicationState } from "../Redux/applicationState";
import { ActionCreators as NavigationContent } from "../Redux/Actions/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../Redux/Actions/getFooterContentAction";
import { ActionCreators as SignoutContent } from "../Redux/Actions/getUserSignoutContentAction";

export default function SignoutPage() 
{
    const dispatch = useDispatch();
    
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const signoutView = useSelector((state: IApplicationState) => state.getUserSignoutContent);

    const getContent = React.useCallback(() =>
    {
        dispatch(NavigationContent.getNavigationContent());
        dispatch(FooterContent.getFooterContent());
        dispatch(SignoutContent.getUserSignoutContent());
    }, [ dispatch ]);

    React.useEffect(() => getContent(), [ getContent ]);

    return (
        <>
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <Container>
                <UserSignout content={signoutView?.content} isLoading={signoutView?.isLoading} />
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}
