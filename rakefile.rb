load 'lib/fuburake.rb'
require 'rubygems/package_task'

solution = FubuRake::Solution.new do |sln|
	sln.compile = {
		:solutionfile => 'src/FubuRake.sln'
	}
				 
	sln.assembly_info = {
		:product_name => "FubuRake",
		:copyright => 'Copyright Some Year by Some Guy'
	}
	
	sln.ripple_enabled = true
	sln.fubudocs_enabled = true
	
	sln.ci_steps = [:create_gem]
	
	sln.compile_step :other_compile, 'src/FubuRake.sln'
	
	sln.precompile = [:fake]
end

desc "Just a fake task for testing"
task :fake do
	puts "I'm the FAKE task running!"
end



desc "Creates the gem for fubu.exe"
task :create_gem do
	cleanDirectory 'pkg'

	Rake::Task[:gem].invoke
end

desc "Replaces the existing installed gem with the new version for local testing"
task :local_gem => [:create_gem] do
	sh 'gem uninstall fuburake'
	Dir.chdir 'pkg'
	sh 'gem install fuburake'
	Dir.chdir '..'
end

spec = Gem::Specification.new do |s|
  s.platform    = Gem::Platform::RUBY
  s.name        = 'fuburake'
  s.version     = solution.options[:build_number]
  s.files += Dir['lib/*.rb']
  s.bindir = 'bin'
  
  s.summary     = 'Rake tasks for fubu related projects'
  s.description = 'Rake helpers for FubuDocs, ripple, NUnit, and cross platform fubu project development'
  
  s.authors           = ['Jeremy D. Miller', 'Josh Arnold', 'Joshua Flanagan']
  s.email             = 'fubumvc-devel@googlegroups.com'
  s.homepage          = 'http://fubu-project.org'
  s.rubyforge_project = 'fuburake'
end


Gem::PackageTask.new(spec) do |pkg|
  pkg.need_zip = true
  pkg.need_tar = true
end