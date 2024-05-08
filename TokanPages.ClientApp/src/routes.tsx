import * as React from "react";
import { Route } from "react-router-dom";
import {
    MainPage,
    StoryPage,
    ArticlesPage,
    TermsPage,
    PolicyPage,
    ContactPage,
    ShowcasePage,
    SigninPage,
    SignupPage,
    SignoutPage,
    AccountPage,
    PasswordResetPage,
    PasswordUpdatePage,
    NewsletterRemovePage,
    NewsletterUpdatePage,
    ActivationPage,
    BicyclePage,
    ElectronicsPage,
    FootballPage,
    GuitarPage,
    PhotographyPage,
} from "./Pages";

export const Routes = (): JSX.Element => {
    return (
        <>
            <Route exact path="/">
                <MainPage />
            </Route>
            <Route exact path="/mystory">
                <StoryPage />
            </Route>
            <Route exact path="/articles">
                <ArticlesPage />
            </Route>
            <Route exact path="/showcase">
                <ShowcasePage />
            </Route>
            <Route exact path="/bicycle">
                <BicyclePage />
            </Route>
            <Route exact path="/electronics">
                <ElectronicsPage />
            </Route>
            <Route exact path="/football">
                <FootballPage />
            </Route>
            <Route exact path="/guitar">
                <GuitarPage />
            </Route>
            <Route exact path="/photography">
                <PhotographyPage />
            </Route>
            <Route exact path="/terms">
                <TermsPage />
            </Route>
            <Route exact path="/policy">
                <PolicyPage />
            </Route>
            <Route exact path="/contact">
                <ContactPage />
            </Route>
            <Route exact path="/signin">
                <SigninPage />
            </Route>
            <Route exact path="/signup">
                <SignupPage />
            </Route>
            <Route exact path="/signout">
                <SignoutPage />
            </Route>
            <Route exact path="/account">
                <AccountPage />
            </Route>
            <Route exact path="/accountactivation">
                <ActivationPage />
            </Route>
            <Route exact path="/updatepassword">
                <PasswordUpdatePage />
            </Route>
            <Route exact path="/resetpassword">
                <PasswordResetPage />
            </Route>
            <Route exact path="/update-newsletter">
                <NewsletterUpdatePage />
            </Route>
            <Route exact path="/remove-newsletter">
                <NewsletterRemovePage />
            </Route>
        </>
    );
};
