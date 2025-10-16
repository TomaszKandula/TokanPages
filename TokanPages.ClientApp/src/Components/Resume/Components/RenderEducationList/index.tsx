import React from "react";
import { EducationItemProps } from "../../../../Api/Models";
import { Link, Skeleton } from "../../../../Shared/Components";
import { ResumeViewProps } from "../..//Types";
import { v4 as uuid } from "uuid";

export const RenderEducationList = (props: ResumeViewProps): React.ReactElement => (
    <>
        {props.page.resume.education.list.map((value: EducationItemProps, _index: number) => (
            <div key={uuid()} className="is-flex is-flex-direction-column mb-4">
                <div className="is-flex is-justify-content-space-between is-align-items-center">
                    <div className="is-flex is-flex-direction-column my-2">
                        <Skeleton isLoading={props.isLoading} width={50} height={24}>
                            <p className="is-size-5 has-text-weight-bold has-text-grey-dark">{value.schoolName}</p>
                        </Skeleton>
                        <Skeleton isLoading={props.isLoading} width={100} height={24}>
                            <p className="is-size-5 has-text-grey">{value.tenureInfo}</p>
                        </Skeleton>
                    </div>
                    <Skeleton isLoading={props.isLoading} width={100} height={24}>
                        <p className="is-size-5 has-text-grey-dark">
                            {value.dateStart} - {value.dateEnd}
                        </p>
                    </Skeleton>
                </div>
                <Skeleton isLoading={props.isLoading} width={300} height={24}>
                    <p className="is-size-5 has-text-grey-dark my-1">{value.details}</p>
                </Skeleton>
                <div className="is-flex is-gap-1.5">
                    <Skeleton isLoading={props.isLoading} height={24}>
                        <p className="is-size-5 has-text-grey-dark my-1">{value.thesis.label}:</p>
                        <Link
                            to={`/${props.languageId}/document?name=${value.thesis.file}&redirect=resume`}
                            className="is-size-5 my-1 is-underlined"
                        >
                            <>{value.thesis.name}</>
                        </Link>
                    </Skeleton>
                </div>
            </div>
        ))}
    </>
);
