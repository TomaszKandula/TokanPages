import * as React from "react";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import ResetForm from "../Components/Account/resetForm";
import Footer from "../Components/Layout/footer";
import { navigationDefault, resetFormDefault } from "../Api/Defaults";
import { getNavigationText, getResetFormText } from "../Api/Services";

export default function ResetPage() 
{
    const mountedRef = React.useRef(true);
    const [resetForm, setResetForm] = React.useState(resetFormDefault);
    const [navigation, setNavigation] = React.useState(navigationDefault);

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setResetForm(await getResetFormText());
        setNavigation(await getNavigationText());
    }, [ ]);

    React.useEffect(() => 
    {
        updateContent();
        return () => { mountedRef.current = false; };
    }, [ updateContent ]);

    return (
        <>     
            <Navigation content={navigation.content} />
            <Container>
                <ResetForm content={resetForm.content} />
            </Container>
            <Footer backgroundColor="#FAFAFA" />
        </>
    );
}
