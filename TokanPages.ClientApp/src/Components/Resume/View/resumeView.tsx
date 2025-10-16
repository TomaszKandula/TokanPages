import React from "react";
import { Media, RenderList, Skeleton } from "../../../Shared/Components";
import { ResumeViewProps } from "../Types";
import {
    RenderCaption,
    RenderEducationList,
    RenderExperienceList,
    RenderInterestsList,
    RenderTestimonials,
    RenderTopHeaderLarge,
    RenderTopHeaderSmall,
} from "../Components";

const RenderResume = (props: ResumeViewProps): React.ReactElement => (
    <>
        <Media.DesktopOnly>
            <RenderTopHeaderSmall {...props} />
        </Media.DesktopOnly>
        <Media.TabletOnly>
            <RenderTopHeaderLarge {...props} />
        </Media.TabletOnly>
        <Media.MobileOnly>
            <RenderTopHeaderLarge {...props} />
        </Media.MobileOnly>
        <RenderCaption isLoading={props.isLoading} text={props.page.resume.summary.caption} tag="h3" />
        <Skeleton isLoading={props.isLoading} mode="Text" height={24} className="my-4">
            <h4 className="is-size-5 has-text-grey-dark has-text-justified line-height-20">
                {props.page.resume.summary.text}
            </h4>
        </Skeleton>
        <RenderCaption isLoading={props.isLoading} text={props.page.resume.achievements.caption} />
        <div className="bulma-content has-text-grey-dark">
            <RenderList
                isLoading={props.isLoading}
                list={props.page.resume.achievements.list}
                className="is-size-5 has-text-justified line-height-20"
            />
        </div>
        <RenderCaption isLoading={props.isLoading} text={props.page.resume.experience.caption} />
        <RenderExperienceList {...props} />
        <RenderCaption isLoading={props.isLoading} text={props.page.resume.education.caption} />
        <RenderEducationList {...props} />
        <RenderCaption isLoading={props.isLoading} text={props.section.caption} />
        <RenderTestimonials {...props} />
        <RenderCaption isLoading={props.isLoading} text={props.page.resume.interests.caption} />
        <RenderInterestsList {...props} />
    </>
);

export const ResumeView = (props: ResumeViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container bulma-is-max-tablet pb-6">
            <Media.DesktopOnly>
                <RenderResume {...props} />
            </Media.DesktopOnly>
            <Media.TabletOnly>
                <div className="mx-4">
                    <RenderResume {...props} />
                </div>
            </Media.TabletOnly>
            <Media.MobileOnly>
                <div className="mx-4">
                    <RenderResume {...props} />
                </div>
            </Media.MobileOnly>
        </div>
    </section>
);
