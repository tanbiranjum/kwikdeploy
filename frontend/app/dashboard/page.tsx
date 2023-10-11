import React from 'react'
import { MainNav } from './components/main-nav'
import { UserNav } from './components/user-nav'

type Props = {}

const DashboardPage = (props: Props) => {
  return (
    <main>
        <div className="border-b">
          <div className="flex h-16 items-center px-4">
            <MainNav className="mx-6" />
            <div className="ml-auto flex items-center space-x-4">
              <UserNav />
            </div>
          </div>
        </div>
    </main>
  )
}

export default DashboardPage