import os

src_path = '.'
bin_path = './bin/'
javac_file = 'cs.bat'

dirlist = []

# If ./bin/ doesn't exist, create it
if (os.path.exists(bin_path) is False):
	os.mkdir(bin_path)

print('Enter in c# flags. Enter \"none" for no flags:')
flags = input('>>> ')

if flags.lower() == "none":
	flags = ""

for x in os.walk(src_path):
	cur_dir = x[0]
	for file in os.listdir(cur_dir):
		if file.endswith('.cs'): # check if the directory has a java file
			dirlist.append(cur_dir.replace('\\','/'))
			break

with open(javac_file, 'w') as file:
	if (flags != ""):
		file.write(''.join(["csc ", flags.strip(), " "]))
	else:
		file.write('csc ')
		
	for dir in dirlist:
		file.write(''.join([dir, "/*.cs "]))
		
print("Successfully wrote c# command into " + javac_file)