use MarketingPostManager
go

select * from MarketingPostManager_ent_Post with (nolock)
select * FROM MarketingPostManager_xref_PostTag with (nolock)
select * from MarketingPostManager_ent_Tag with (nolock)
select * from MarketingPostManager_xref_PostGroup with (nolock)
select * from MarketingPostManager_enu_Group with (nolock)

/*
declare @postId INT = 14
delete from MarketingPostManager_xref_PostTag where postid = @postId
delete from MarketingPostManager_xref_PostGroup where postid = @postId
delete from MarketingPostManager_ent_Post where postid = @postId
*/
