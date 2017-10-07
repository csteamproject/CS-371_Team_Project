###############################################################################
								CS371 Team Project Git
	This document is intended to be used as a rough guide to using git.
###############################################################################

Cloning into a directory
	
	The online repository is set up in a way that should make it difficult for us
	to alter or destroy the main branch. Because of this, we should always have a
	working demo for the presentations.

	Cloning from the repository into your local directory should pull
	specifically from the dev branch listed on github.

		$ git clone <url>


Creating a new branch

	Once you have cloned the project into your local directory, you can run the
	command
		
		$ git branch

	to see what branch you are on locally. You should see
		
		$ *master

	as the local branch. Create a new branch with the command

		$ git branch dev

	if you run git branch again, you should now see something like

		$ *master
		$	dev

	Switch to the dev branch with checkout.

		$ git checkout dev

	Now you will be on the dev branch in your local directory. From here, you
	should create a new branch that has details of the feature you intend to
	implement. If you are working on the game screen you would do something like

		$ git branch feature_game_screen


Merging your git branches
	
	To prevent merge errors, there is a set of rules you should follow when
	attempting to merge back into the online repository.

	Use
		
		$ git status

	to see what files you have created that are currently not being tracked by
	git. From there you can either run (WARNING: This does not add newly created
	files to tracking - You need to run git add -A BEFORE using this command)

		$ git commit -am "Working game screen"

	to stage these files and prepare them for merging. If you have added NEW
	files in this branch, such as a .png or a new class, you will need to run

		$ git add -A

	BEFORE committing your changes. Now, from your feature branch, you can pull
	the changes from the online repository.

		$ git pull origin dev

	This will grab any changes that have been submitted while you were working on
	your own branch. If you try to merge back into the dev branch without
	grabbing the new changes, you will receive errors. Don't force merges if you
	receive errors.

	Now you want to push your feature branch to the online repository using 

		$ git push origin <feature branch name>

	You should get a URL to github that brings up a pull request. From there you
	just need to target the branch you want to merge into (in our case it's dev
	and probably nothing more complicated) and that's it. FINISHED! You're done.
	Go kill yourself.

	After that, we will review changes and discuss the new stuff being merged and
	merge to our dev branch. Then we test, bug fix, and that becomes the new
	master if all is good.

#################################################################################
